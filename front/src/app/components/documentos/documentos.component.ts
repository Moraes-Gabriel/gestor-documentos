import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Documento } from 'src/app/models/Documento';
import { DocumentosService } from 'src/app/services/documentos.service';

@Component({
  selector: 'app-documentos',
  templateUrl: './documentos.component.html',
  styleUrls: ['./documentos.component.scss']
})
export class DocumentosComponent implements OnInit {

  constructor() { }

  
  
  ngOnInit(): void {
    
  }
 

}
