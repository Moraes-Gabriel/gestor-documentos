import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/Pagination';
import { Tipo } from '../models/Tipo';

@Injectable({
  providedIn: 'root'
})
export class TipoService {

  baseUrl = environment.apiURL + "tipo"
  constructor(private http: HttpClient) { }


  public getAll(): Observable<Tipo[]> {
    return this.http.get<Tipo[]>(this.baseUrl).pipe(
      take(1),
      map((response) => {
        return response;
      })
    )
  }
  public getAllPaginado(
    page?: number, itemsPerPage?: number, ):
     Observable<PaginatedResult<Tipo[]>> {

    const paginatedResult: PaginatedResult<Tipo[]>= new PaginatedResult<Tipo[]>();
    let params = new HttpParams;

      if (page != null && itemsPerPage != null) {
        params = params.append('pageNumber', page.toString());
        params = params.append('pageSize', itemsPerPage.toString());
      }

    return this.http.get<any>(`${this.baseUrl}/paginado`, { observe: 'response', params})
    .pipe(take(1),
    map((response) => {
      paginatedResult.result = response.body;
        if(response.headers.has('Pagination')) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') || '{}');
        }
        return paginatedResult;
    }));
  }

  
  public post(tipo: Tipo): Observable<any> {
    return this.http
      .post<any>(this.baseUrl, tipo)
      .pipe(take(1));
  }

  public put(tipo: Tipo): Observable<any> {
    return this.http
      .post<any>(this.baseUrl, tipo)
      .pipe(take(1));
  }
  
  
  public getById(id:number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`).pipe(
      take(1),
      map((response: any) => {
        return response.result;
      })
    )
  }

  public delete(id:number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${id}`).pipe(
      take(1),
      map((response) => {
        return response.result;
      })
    )
  }

}
