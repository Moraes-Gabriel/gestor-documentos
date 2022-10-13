import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  selector: 'app-meus-documentos-tabela',
  templateUrl: './meus-documentos-tabela.component.html',
  styleUrls: ['./meus-documentos-tabela.component.scss', "../tabela-documentos.scss"]
})
export class MeusDocumentosTabelaComponent implements OnInit {

  documentos = {} as Documento[];
  documentosFiltrados = {} as Documento[] || null;
  documentoPageRequest = {} as DocumentoPageRequest;
  pagination = {} as Pagination;

  constructor(private docService: DocumentosService, private tipoService: TipoService,
    private conService: ConcessaoService, private toastr: ToastrService, private router: Router,
    public accountService: AccountService
    ) { }

  concessaoSelect = {} as Concessao[];
  tipoSelect = {} as Tipo[];

  concessaoSelecionado!: number | null;
  tipoSelecionado!: number | null;

  ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 1,
    } as Pagination;

    this.conService.getAll().subscribe((response) => this.concessaoSelect = response);
    this.tipoService.getAll().subscribe((response) => this.tipoSelect = response);

    this.carregarDocumentos();
  }

  carregarDocumentos():void {
    this.docService.getAllDoUsuario
    (this.pagination.currentPage, this.pagination.itemsPerPage, this.filtroLista, this.concessaoSelecionado, this.tipoSelecionado)
    .subscribe((paginatedResult: PaginatedResult<Documento[]>) => {
      this.documentos = paginatedResult.result;
      this.documentosFiltrados = paginatedResult.result;
      this.pagination = paginatedResult.pagination;
    },
      (error: any) => {
        console.error(error);
        this.toastr.error('Error todos os documentos', 'Erro');
      });
  }
  editarDocumento(documentoId:number) {
    this.router.navigateByUrl(`documentos/criar/${documentoId}`)
  }

  public pageChanged(event: any) {
    this.pagination.currentPage = event.page;
    this.carregarDocumentos();
  }
  
  private _filtroLista: string = "";

  public get filtroLista() {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
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

}
