import { Component, OnInit } from '@angular/core';
import { Documento } from 'src/app/models/Documento';
import { DocumentosService } from 'src/app/services/documentos.service';

@Component({
  selector: 'app-modal-documento-cadastrado',
  templateUrl: './modal-documento-cadastrado.component.html',
  styleUrls: ['./modal-documento-cadastrado.component.scss']
})
export class ModalDocumentoCadastradoComponent implements OnInit {

  documento!: Documento;
  constructor(private docService: DocumentosService) { }

  ngOnInit(): void {
    this.docService.getById(1).subscribe((response) => this.documento = response);
  }

}
