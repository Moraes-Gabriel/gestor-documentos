import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-admin',
  templateUrl: './nav-admin.component.html',
  styleUrls: ['./nav-admin.component.scss']
})
export class NavAdminComponent implements OnInit {



  ngOnInit(): void {
  }

  docSelecionado = true;
  tipoSelecionado = false;
  conSelecionado = false;

  corDocF() {
    this.docSelecionado = true;
    this.tipoSelecionado = false;
    this.conSelecionado = false;
  }
  corConF() {
   
    this.docSelecionado = false;
    this.tipoSelecionado = false;
    this.conSelecionado = true;

   
  }

  corTipoF() {
    

    this.docSelecionado = false;
    this.tipoSelecionado = true;
    this.conSelecionado = false;

  }
}
