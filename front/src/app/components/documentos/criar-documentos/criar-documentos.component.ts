import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Concessao } from 'src/app/models/Concessao';
import { Documento } from 'src/app/models/Documento';
import { Tipo } from 'src/app/models/Tipo';
import { ConcessaoService } from 'src/app/services/concessao.service';
import { DocumentosService } from 'src/app/services/documentos.service';
import { TipoService } from 'src/app/services/tipo.service';

@Component({
  selector: 'app-criar-documentos',
  templateUrl: './criar-documentos.component.html',
  styleUrls: ['./criar-documentos.component.scss']
})
export class CriarDocumentosComponent implements OnInit {

  modalRef!: BsModalRef;
  concessaoSelect = {} as Concessao[];
  tipoSelect = {} as Tipo[];
  form!: FormGroup;
  documento!: Documento;
  documentoId!: number;
  titulo: String =  "Criar documento";
  tituloBotao: String =  "Gerar numero documento";
  status: String = "criar";
  estadoSalvar: any = "post";

  constructor(private fb: FormBuilder,
    private docService: DocumentosService, private tipoService: TipoService, 
    private conService: ConcessaoService,private toastr: ToastrService,
    private router: Router,private route: ActivatedRoute, private modalService: BsModalService
    ) { 
      this.route.params.subscribe(params => this.documentoId = params['id']);
    }

    public carregarDocumento(): void {
    
    if (this.documentoId !== null && this.documentoId !== undefined && this.documentoId !== 0) {
      this.estadoSalvar = 'put';
      this.status = "editar";
      this.titulo = "Editar documento";
      this.tituloBotao = "Editar documento";

      this.docService
          .getById(this.documentoId)
          .subscribe(
            (documento: Documento) => {
              this.documento = { ...documento };
              
              this.form.patchValue({ descricao: this.documento.descricao, 
              tipoId: this.documento.tipo.id,
               concessaoId: this.documento.concessao.id,
                concessao: this.documento.concessao,
                tipo: this.documento.tipo,
              });
            },
            (error: any) => {
              this.toastr.error('Erro ao tentar carregar Evento.', 'Erro!');
              console.error(error);
            }
          )
      }
    }

    get f(): any { return this.form.controls; }
 
  ngOnInit(): void {
    this.carregarDocumento();
    this.validation();
    this.conService.getAll().subscribe((response) => this.concessaoSelect = response);
    this.tipoService.getAll().subscribe((response) => this.tipoSelect = response);
   }

  private validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required,Validators.minLength(6),Validators.maxLength(100)]],
      concessaoId: ['', Validators.required],
      tipoId: ['', Validators.required],
    });
  }

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  salvarDocumento(): void {
    this.documento =
       this.estadoSalvar === 'post'
      ?{...this.form.value} : { id: this.documento.id, ...this.form.value };
    
      if(this.estadoSalvar === 'post') {
        
        this.docService["post"](this.documento).subscribe((response) => {
          this.router.navigate([`documentos/pdf/${response.value}`])
          this.showSuccess();
        })
      }else if(this.estadoSalvar === 'put'){ 
        this.docService["put"](this.documento).subscribe((response) => {
          
          this.router.navigateByUrl(`documentos/pdf/${this.documentoId}`)
          this.showSuccess();
        })
      }
    }
    
    openModal(event: any, template: TemplateRef<any>): void {
      event.stopPropagation();
      this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
    }
    
    confirm() {
      this.docService.delete(this.documentoId).subscribe(
        () => {
          this.toastr.success("sucesso ao deletar o documento", "documento deletado");
          this.router.navigate([`documentos/meus`]);
          this.modalRef.hide();
      },
        () => this.toastr.error("erro ao deletar o documento","erro ao deletar")
    )
    
  }
  decline() {
    this.modalRef.hide();
  }

  showSuccess() {
    this.toastr.success('Você teve sucesso ao criar o documento', 'Documento criado');
  }
}
