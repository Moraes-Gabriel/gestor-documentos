import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/models/Pagination';
import { Tipo } from 'src/app/models/Tipo';
import { TipoService } from 'src/app/services/tipo.service';

@Component({
  selector: 'app-tabela-tipo',
  templateUrl: './tabela-tipo.component.html',
  styleUrls: ['./tabela-tipo.component.scss']
})
export class TabelaTipoComponent implements OnInit {

  pagination = {} as Pagination;
  
  tipos = {} as Tipo[];
  constructor(private tipoService: TipoService, private router: Router) { }

  ngOnInit(): void {

    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 1,
    } as Pagination;

    
    this.carregarTipos();
  }

  carregarTipos():void {
    this.tipoService.getAllPaginado(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((paginatedResult: PaginatedResult<Tipo[]>) =>{
      this.tipos = paginatedResult.result;
      this.pagination = paginatedResult.pagination;

    });
  }
  
  public pageChanged(event: any) {
    this.pagination.currentPage = event.page;
    this.carregarTipos();
  }

  editarTipo(id: number) {
    
    this.router.navigateByUrl(`admin/tipos/criar/${id}`)
  }

}
