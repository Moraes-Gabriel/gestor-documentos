import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';
import { AccountService } from 'src/app/services/account.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginGoogleComponent implements OnInit {

  private clientId = "42523927962-qh268urhb8n1hv3amivo69a32ofd0hkj.apps.googleusercontent.com"

  constructor(
    private router: Router,
    private service: AuthService,
    private _ngZone: NgZone,
    public accountService: AccountService) { }

  ngOnInit(): void {
    // @ts-ignore
    window.onGoogleLibraryLoad = () => {
      // @ts-ignore
      google.accounts.id.initialize({
        client_id: this.clientId,
        callback: this.handleCredentialResponse.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true
      });
      // @ts-ignore
      google.accounts.id.renderButton(
        // @ts-ignore
        document.getElementById("buttonDiv"),
        { theme: "outline", size: "large", width: "100%" }
      );
      // @ts-ignore
      google.accounts.id.prompt((notification: PromptMomentNotification) => { });
    };
  }

  async handleCredentialResponse(response: CredentialResponse) {
    await this.service.LoginWithGoogle(response.credential).subscribe(
      (x: any) => {
        localStorage.setItem("token", x.token);
        const user = x;
        if (user) {
          this.accountService.setCurrentUser(user);
        }
        this._ngZone.run(() => {
          this.router.navigate(['/documentos/meus']);
        })
      },
      (error: any) => {
        console.log(error);
      }
    );
  }
}
