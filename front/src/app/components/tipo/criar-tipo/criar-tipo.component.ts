import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Tipo } from 'src/app/models/Tipo';
import { TipoService } from 'src/app/services/tipo.service';

@Component({
  selector: 'app-criar-tipo',
  templateUrl: './criar-tipo.component.html',
  styleUrls: ['./criar-tipo.component.scss']
})
export class CriarTipoComponent implements OnInit {

  modalRef!: BsModalRef;
  form!: FormGroup;
  titulo = "Adicionar tipo"
  tituloBotao = "Salvar tipo"
  
  tipoId!: number;
  tipo!: Tipo;

  estadoSalvar = 'post'

  constructor(private fb: FormBuilder,
    private tipoService: TipoService, private toastr: ToastrService,
    private route: ActivatedRoute,private modalService: BsModalService,
    private router: Router
  ) {
    this.route.params.subscribe(params => this.tipoId = params['id']);
  }

  get f(): any { 
    
    return this.form.controls; }

  ngOnInit(): void {
    this.carregarDocumento();
    this.validation();
  }

  salvarTipo(): void {

    this.tipo =
      this.estadoSalvar === 'post'
        ? { ...this.form.value } : { id: this.tipo.id, ...this.form.value };


    
    if (this.estadoSalvar === 'post'|| this.estadoSalvar === 'put') {
      this.tipoService[this.estadoSalvar](this.tipo).subscribe(
        () => {
          this.estadoSalvar == 'post' ? 
          this.toastr.success("sucesso ao criar um tipo", " sucesso") :
           this.toastr.success("sucesso ao editar uma tipo", " sucesso") 
           this.router.navigateByUrl('admin/tipos/indice')
        }, 
        () => {
          this.estadoSalvar == 'post' ?
          this.toastr.error("houve um erro ao criar uma tipo", "erro") :
            this.toastr.error("houve um erro ao editar uma tipo", "erro") 
      })
    } 
  }

  openModal(event: any, template: TemplateRef<any>): void {
    event.stopPropagation();
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }
  
  confirm() {

    this.tipoService.delete(this.tipoId).subscribe(() => {
      this.toastr.error("erro ao deletar o evento", "evento deletado")
    })
    
  }
  decline() {
    this.modalRef.hide();
  }


  public carregarDocumento(): void {

    if (this.tipoId !== null && this.tipoId !== undefined && this.tipoId !== 0) {
      this.titulo = "Editar documento";
      this.tituloBotao = "Editar documento";
      this.estadoSalvar = 'put';
      
      
      this.tipoService.getById(this.tipoId)
        .subscribe(
          (tipo: Tipo) => {
            this.tipo = { ...tipo };
            this.form.patchValue(this.tipo);
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar carregar tipo.', 'Erro!');
            console.error(error);
          }
        )
    }
  }


  private async validation(): Promise<any> {
    this.form = this.fb.group({
      nome: ['', Validators.required, Validators.maxLength(15)],
      sigla: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(3)]],
    });
  }
}

