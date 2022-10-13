import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AccordionComponent } from 'ngx-bootstrap/accordion';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private toaster: ToastrService,
    public accountService: AccountService
  ) {}

  canActivate(): boolean {
    if (localStorage.getItem('user') !== null)
    return true;
    
    this.toaster.info('Usuário não autenticado!');
    this.router.navigateByUrl('user/login');
    return false;
  }

}
