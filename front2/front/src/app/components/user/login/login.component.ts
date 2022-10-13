import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { User } from 'src/app/models/identity/User';
import { UserLogin } from 'src/app/models/identity/UserLogin';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  model = {} as UserLogin;
  
  constructor(public accountService: AccountService,
    private router: Router, private toastr: ToastrService
    ) { }

   
  ngOnInit(): void {
  }

  public login(): void {
    this.accountService.login(this.model).subscribe(
      () => {
        this.showSuccess();
        this.router.navigateByUrl('/documentos/meus');
        
      },
      (error:any) => {
        if(error.status == 401 || error.status == 404)
          this.showError();
        else console.error(error);
      }
    )
  }

  showSuccess() {
    this.toastr.success('Você teve sucesso logar na conta', 'Logado com sucesso');
  }
  showError() {
    this.toastr.success('Usuario ou senha inválido', 'Não foi possivel logar');
  }
  
}
