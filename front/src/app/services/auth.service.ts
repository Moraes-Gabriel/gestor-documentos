import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private path = environment.apiURL

  constructor(private httpClient: HttpClient) { }

  public signOutExternal = () => {
      localStorage.removeItem("token");
  }

  LoginWithGoogle(credentials: string): Observable<any> {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post(this.path + "usuario/LoginWithGoogle", JSON.stringify(credentials), { headers: header });
  }


  }
