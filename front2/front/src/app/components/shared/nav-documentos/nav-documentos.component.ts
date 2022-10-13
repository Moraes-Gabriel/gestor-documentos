import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-documentos',
  templateUrl: './nav-documentos.component.html',
  styleUrls: ['./nav-documentos.component.scss']
})
export class NavDocumentosComponent implements OnInit {

  corAll: string = '#e9eff4';
  corMeu: string = 'white';


  corMeuFunc() {
    this.corAll = "#e9eff4"; 
    this.corMeu = "white"; 
  }
  corAllFunc(){
    this.corAll = "white"; 
    this.corMeu = "#e9eff4"; 
  }
  
  nenhumaCorFunc(){
    this.corAll = "#e9eff4"; 
    this.corMeu = "#e9eff4"; 
  }
  constructor(private router: Router) { }

  ngOnInit(): void {
  }
  showMenu(): Boolean {
    return this.router.url !== '/user/login';
  }
}
