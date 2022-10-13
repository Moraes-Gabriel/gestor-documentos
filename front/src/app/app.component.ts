import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { User } from './models/identity/User';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  modeAdmin = false;
  display = "block";
  userBool: User | null = null;

  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    
    this.setCurrentUser();

  }
  mudarModeAdmin($event: any) {
    this.modeAdmin = !this.modeAdmin;
    this.display = this.modeAdmin == true ? "flex" : "block";
  }

  setCurrentUser(): void {


    this.accountService.currentUser$.subscribe(
      (r) => {

      },
      (R) => {  
       },
      () => {
        
        this.display = 'block';
        this.modeAdmin = false;
        },
    )

    let user: User | null;

    if (localStorage.getItem('user')) 
    {
      user = JSON.parse(localStorage.getItem('user') ?? '{}');
      this.userBool = user;
    } else {
      user = null
    }

    if (user) {
      this.accountService.setCurrentUser(user);
    }

  }

}
