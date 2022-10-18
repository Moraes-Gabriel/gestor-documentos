import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router, RouteReuseStrategy } from '@angular/router';
import { take } from 'rxjs';
import { User } from 'src/app/models/identity/User';
import { Usuario } from 'src/app/models/identity/usuario';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() outPutModeAdmin = new EventEmitter<Boolean>();


  modeAdmin = false;
  user: Usuario | null | undefined;
  constructor(public accountService: AccountService,
              private router: Router) { }
              
  showMenu(): Boolean {
    return this.router.url !== '/user/login' && this.router.url !== '/user/login/google';
  }
  
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('app');
  }
  mudarModoAdmin() {
    this.outPutModeAdmin.emit(!this.modeAdmin);
    if(!this.modeAdmin === true) this.router.navigateByUrl('admin/documentos/indice')
  }
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user => {
      this.user = user?.user 
    });
  }

}
