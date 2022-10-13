import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Concessao } from 'src/app/models/Concessao';
import { ConcessaoService } from 'src/app/services/concessao.service';

@Component({
  selector: 'app-criar-concessao',
  templateUrl: './criar-concessao.component.html',
  styleUrls: ['./criar-concessao.component.scss']
})
export class CriarConcessaoComponent implements OnInit {

  modalRef!: BsModalRef;
  form!: FormGroup;
  titulo = "Adicionar concessao"
  tituloBotao = "Salvar concessao"
  concessaoId!: number;
  concessao!: Concessao;

  estadoSalvar = 'post'

  constructor(private fb: FormBuilder,
    private conService: ConcessaoService, private toastr: ToastrService,
    private route: ActivatedRoute, private modalService: BsModalService,
    private router: Router
  ) {
    this.route.params.subscribe(params => this.concessaoId = params['id']);
  }

  get f(): any { return this.form.controls; }

  ngOnInit(): void {
    this.carregarDocumento();
    this.validation();
  }
  
  salvarConcessao(): void {
    
    this.concessao =
      this.estadoSalvar === 'post'
        ? { ...this.form.value } : { id: this.concessao.id, ...this.form.value };

    if (this.estadoSalvar === 'post' || this.estadoSalvar === 'put') {
      
      this.conService[this.estadoSalvar](this.concessao).subscribe(
        () => {
        this.estadoSalvar == 'post' ? 
          this.toastr.success("sucesso ao criar um concessão", " sucesso") :
           this.toastr.success("sucesso ao editar uma concessão", " sucesso") 
           this.router.navigateByUrl('admin/concessoes/indice')
      }, () => {
        this.estadoSalvar == 'post' ?
          this.toastr.error("houve um erro ao criar uma concessão", "erro") :
            this.toastr.error("houve um erro ao editar uma concessão", "erro") 
      })
    } 
  }

  public carregarDocumento(): void {
    if (this.concessaoId !== null && this.concessaoId !== undefined && this.concessaoId !== 0) {
      this.titulo = "Editar concessão";
      this.tituloBotao = "Editar concessão";
      this.estadoSalvar = 'put';
      
      this.conService
        .getById(this.concessaoId)
        .subscribe(
          (concessao: Concessao) => {
            this.concessao = { ...concessao };
            this.form.patchValue({ ...this.concessao})
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar carregar concessão.', 'Erro!');
            console.error(error);
          }
        )
    }
  }


  openModal(event: any, template: TemplateRef<any>): void {
    event.stopPropagation();
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }
  
  confirm() {

    this.conService.delete(this.concessaoId).subscribe((response) => {
      this.toastr.success("deletado", "deletado")
    }
      )
    
  }
  decline() {
    this.modalRef.hide();
  }

  private async validation(): Promise<any> {
    this.form = await this.fb.group({
      nome: ['', Validators.required, Validators.maxLength(15)],
      sigla: ['', [Validators.required, Validators.maxLength(3)]],
    });
  }
}
