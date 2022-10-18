import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Documento } from 'src/app/models/Documento';
import { DocumentosService } from 'src/app/services/documentos.service';

@Component({
  selector: 'app-criar-documento-pdf',
  templateUrl: './criar-documento-pdf.component.html',
  styleUrls: ['./criar-documento-pdf.component.scss']
})
export class CriarDocumentoPdfComponent implements OnInit {

  documento= {} as Documento;
  documentoId!: number;
  titulo = "CRIAR DOCUMENTO";
  shortLink: string = "";
  loading: boolean = false; // Flag variable
  pdfDoc: any;
  file!: File[] | null; // Variable to store file

  onFileSelected() {
    let $img: any = document.querySelector('#file');
    
    if (typeof (FileReader) !== 'undefined') {
      let reader = new FileReader();
      reader.onload = (e: any) => {
        this.pdfDoc = e.target.result;
      };
  
      reader.readAsArrayBuffer($img.files[0]);
    }
  }

  constructor(
    private docService: DocumentosService,
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    private toastr: ToastrService,
  ) {
    this.route.params.subscribe(params => this.documentoId = params['id']);
  }

  ngOnInit(): void {
    this.docService.getById(this.documentoId).subscribe((response) => {
      this.documento = response;

      if(response.urlArquivoS3){
        let file = this.docService.getUrlFile(response.id);
        this.pdfDoc = file;
        this.file = file;
        this.titulo = "EDITAR DOCUMENTO";
      }
    })
  }


  deletarPdf() {
    this.file = null;
  }

  // Inject service 


  // On file Select
  onChange(event: any) {
    this.file = event.target.files[0];
  }

  onClickAdicionarPdfAoDocumento() {
    var file: any = new FormData();
    file.append("file", this.file);

    this.docService.postFile(this.documentoId, file).subscribe((response) => {
      this.toastr.success("sucesso", "sucesso ao enviar o pdf")
    }, (error) => {
      this.toastr.error("Erro", "Erro ao criar enviar o pdf")
    })
  }
}
