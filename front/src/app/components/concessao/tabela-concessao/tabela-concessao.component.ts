import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Concessao } from 'src/app/models/Concessao';
import { PaginatedResult, Pagination } from 'src/app/models/Pagination';
import { ConcessaoService } from 'src/app/services/concessao.service';

@Component({
  selector: 'app-tabela-concessao',
  templateUrl: './tabela-concessao.component.html',
  styleUrls: ['./tabela-concessao.component.scss']
})
export class TabelaConcessaoComponent implements OnInit {
  
  pagination = {} as Pagination;
  concessoes = {} as Concessao[];
  constructor(private conService: ConcessaoService, private router: Router) { }

  ngOnInit(): void {

    this.pagination = {
      currentPage: 1,
      itemsPerPage: 10,
      totalItems: 1,
    } as Pagination;

    this.conService.getAll().subscribe((response) => this.concessoes = response);
  }

  carregarConcessoes():void {
    this.conService.getAllPaginado(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((paginatedResult: PaginatedResult<Concessao[]>) =>{
      this.concessoes = paginatedResult.result;
      this.pagination = paginatedResult.pagination;

    });
  }
  public pageChanged(event: any) {
    this.pagination.currentPage = event.page;
    this.carregarConcessoes();
  }
  


  editarConcessao(id: number){
     this.router.navigateByUrl(`admin/concessoes/criar/${id}`)
  }
}
