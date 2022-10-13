import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../models/identity/User';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardAdmin implements CanActivate {
  constructor(
    private router: Router,
    private toaster: ToastrService,
    public accountService: AccountService
  ) {}

  canActivate(): boolean {
    let ls: User;

    
    ls = JSON.parse(localStorage.getItem('user') || '{}');
      if(ls.user?.role === 'admin') return true;

    this.toaster.info('Usuário não é admin!');
    this.router.navigate(['/user/login']);
    return false;
  }

}
