import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, Subject } from 'rxjs';
import { Concessao } from 'src/app/models/Concessao';
import { Documento } from 'src/app/models/Documento';
import { DocumentoPageRequest } from 'src/app/models/DocumentoPageRequest';
import { User } from 'src/app/models/identity/User';
import { PaginatedResult, Pagination } from 'src/app/models/Pagination';
import { Tipo } from 'src/app/models/Tipo';
import { AccountService } from 'src/app/services/account.service';
import { ConcessaoService } from 'src/app/services/concessao.service';
import { DocumentosService } from 'src/app/services/documentos.service';
import { TipoService } from 'src/app/services/tipo.service';

@Component({
  selector: 'app-tabela-documentos',
  templateUrl: './tabela-documentos.component.html',
  styleUrls: ['./tabela-documentos.component.scss', "../tabela-documentos.scss"]
})
export class TabelaDocumentosComponent implements OnInit {

  date = Date.now();

  data = "2003/04/03"
  documentos: Documento[] = [];
  documentoPageRequest = {} as DocumentoPageRequest;
  pagination = {} as Pagination;

  concessaoSelect!: Concessao[];
  tipoSelect!: Tipo[];

  concessaoSelecionado!: number | null;
  tipoSelecionado!: number | null;
  descricaoFiltrada: any;

  estadoGet: string = 'getAll';
  mode! : string;

  titulo!: string;
  constructor(private docService: DocumentosService, private tipoService: TipoService,
    private conService: ConcessaoService, private toastr: ToastrService, private router: Router,
    public accountService: AccountService
  ) { }

  ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 1,
    } as Pagination;

    this.conService.getAll().subscribe((response) => this.concessaoSelect = response);
    this.tipoService.getAll().subscribe((response) => this.tipoSelect = response);
    this.mudarCoisas();
    this.carregarDocumentos();
  }

  mudarCoisas() {

    let url = this.router.url;

    
    if (url == '/documentos/meus') {
      this.estadoGet = 'getAllDoUsuario'; this.mode = 'meus'; this.titulo = 'Meus Documentos'}

    if (url == '/documentos/indice') {this.mode = 'todos'; this.titulo = 'Indice Documentos'};

    if (url == '/admin/documentos/indice') {this.mode = 'admin'; this.titulo = 'Indice Documentos'}

  }
  carregarDocumentos(): void {

    if (this.estadoGet === 'getAll' || this.estadoGet === 'getAllDoUsuario') {
      
      this.docService[this.estadoGet]
        (this.pagination.currentPage, this.pagination.itemsPerPage, this.descricaoFiltrada, this.concessaoSelecionado, this.tipoSelecionado)
        .subscribe((paginatedResult: PaginatedResult<Documento[]>) => {
          this.sucessToastr();
          
          this.documentos = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
        },
          () => {
            this.errorToastr()
          });
    }

  }

  editarDocumento(documentoId: number) {
    this.router.navigateByUrl(`documentos/editar/${documentoId}`)
  }

  public pageChanged(event: any) {
    this.pagination.currentPage = event.page;
    this.carregarDocumentos();
  }

  vizualizarEvento(documentoId: number) {
    this.router.navigateByUrl(`documentos/detalhes/${documentoId}`)
  }
  descricaoBuscaChanged: Subject<string> = new Subject<string>();

  public filtrarDocumentos(evt: any): void {

    if (this.descricaoBuscaChanged.observers.length === 0) {
      this.descricaoBuscaChanged.pipe(debounceTime(1500)).subscribe(
        filtrarPor => {
          this.descricaoFiltrada = filtrarPor;

          this.carregarDocumentos();
        }
      )
    }
    this.descricaoBuscaChanged.next(evt.value);
  }

  onChangeConcessao(deviceValue: any) {
    deviceValue.target.value !== "Concessao" ?
      this.concessaoSelecionado = deviceValue.target.value :
      this.concessaoSelecionado = null
    this.carregarDocumentos();
  }
  onChangeTipo(deviceValue: any) {
    deviceValue.target.value !== "Tipo" ?
      this.tipoSelecionado = deviceValue.target.value :
      this.tipoSelecionado = null
    this.carregarDocumentos();
  }


  sucessToastr() {
    this.toastr.success('Sucesso ao caregar os documentos', 'Sucesso');

  }
  errorToastr() {
    this.toastr.error('Error ao caregar os documentos', 'Erro');
  }
}
