import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Documento } from 'src/app/models/Documento';
import { DocumentosService } from 'src/app/services/documentos.service';

@Component({
  selector: 'app-detalhe-documento',
  templateUrl: './detalhe-documento.component.html',
  styleUrls: ['./detalhe-documento.component.scss']
})
export class DetalheDocumentoComponent implements OnInit {

  documento = {} as Documento;
  documentoId!: number;
  imageDoc: any = null;

  constructor(
    private docService: DocumentosService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
  ) {
    this.route.params.subscribe(params => this.documentoId = params['id']);

  }

  async ngOnInit(): Promise<void> {
    await this.docService.getById(this.documentoId).subscribe((response) => {
      this.documento = response;
      if (response.urlArquivoS3) {
        this.imageDoc = this.docService.getUrlFile(this.documentoId);
      }
    });
  }
}
