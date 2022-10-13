import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Concessao } from '../models/Concessao';
import { PaginatedResult } from '../models/Pagination';

@Injectable({
  providedIn: 'root'
})
export class ConcessaoService {

  baseUrl = environment.apiURL + "concenssao"
  constructor(private http: HttpClient) { }


  public getAll(): Observable<Concessao[]> {
    return this.http.get<Concessao[]>(this.baseUrl).pipe(take(1),
    map((response) => {
      return response;
    }))
  }
  public getAllPaginado(
    page?: number, itemsPerPage?: number, ):
     Observable<PaginatedResult<Concessao[]>> {

    const paginatedResult: PaginatedResult<Concessao[]>= new PaginatedResult<Concessao[]>();
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
  public post(concessao: Concessao): Observable<any> {
    return this.http
      .post<any>(this.baseUrl, concessao)
      .pipe(take(1));
  }

  public put(concessao: Concessao): Observable<any> {
    return this.http
      .put<any>(`${this.baseUrl}/${concessao.id}`, concessao)
      .pipe(take(1));
  }

  public getById(id:number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`).pipe(
      take(1),
      map((response) => {
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