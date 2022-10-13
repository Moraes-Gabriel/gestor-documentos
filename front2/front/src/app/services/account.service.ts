import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, ReplaySubject, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/identity/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSource  = new ReplaySubject<any>(1);
  public currentUser$ = this.currentUserSource.asObservable();

  baseUrl = environment.apiURL + 'usuario';

  constructor(private http: HttpClient) {
}

  public login(model: any ): Observable<void> {
    return this.http.post<User>(this.baseUrl + "/login", model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if(user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  public register(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl, model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user)
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentUserSource.complete();
    
  }
  public setCurrentUser(user: User): void {
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user);
  }
  
}
