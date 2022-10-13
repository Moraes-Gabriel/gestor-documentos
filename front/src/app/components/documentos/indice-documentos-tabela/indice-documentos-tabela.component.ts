import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, Subject } from 'rxjs';
import { Concessao } from 'src/app/models/Concessao';
import { Documento } from 'src/app/models/Documento';
import { DocumentoPageRequest } from 'src/app/models/DocumentoPageRequest';
import { PaginatedResult, Pagination } from 'src/app/models/Pagination';
import { Tipo } from 'src/app/models/Tipo';
import { ConcessaoService } from 'src/app/services/concessao.service';
import { DocumentosService } from 'src/app/services/documentos.service';
import { TipoService } from 'src/app/services/tipo.service';

@Component({
  selector: 'app-indice-documentos-tabela',
  templateUrl: './indice-documentos-tabela.component.html',
  styleUrls: ['./indice-documentos-tabela.component.scss', "../tabela-documentos.scss"]
})
export class IndiceDocumentosTabelaComponent implements OnInit {

  documentos = {} as Documento[];
  documentosFiltrados = {} as Documento[];
  pagination = {} as Pagination;

  constructor(private docService: DocumentosService, private tipoService: TipoService,
    private conService: ConcessaoService, private toastr: ToastrService, private router: Router) { }

  concessaoSelect = {} as Concessao[];
  tipoSelect = {} as Tipo[];

  concessaoSelecionado!: number | null;
  tipoSelecionado!: number | null;
  descricaoFiltrada!: string;

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

  carregarDocumentos(): void {
    this.docService.getAll
      (this.pagination.currentPage, this.pagination.itemsPerPage,
        this.descricaoFiltrada, this.concessaoSelecionado, this.tipoSelecionado)
      .subscribe((paginatedResult: PaginatedResult<Documento[]>) => {
        this.documentos = paginatedResult.result;
        this.pagination = paginatedResult.pagination;
      },
        (error: any) => {
          this.toastr.error('Erro ao buscar todos os seus documentos', 'Erro');
        })
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
}
