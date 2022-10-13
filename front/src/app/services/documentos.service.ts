import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Concessao } from '../models/Concessao';
import { Documento } from '../models/Documento';
import { DocumentoPageRequest } from '../models/DocumentoPageRequest';
import { PaginatedResult } from '../models/Pagination';

@Injectable({
  providedIn: 'root'
})
export class DocumentosService {


  getById(documentoId: number) {
    return this.http.get<Documento>(`${this.baseUrl}/${documentoId}` ).pipe(
      take(1),
      map((response) => {
      return response
    })
    )
  }

  
  baseUrl = environment.apiURL + 'documento';
  constructor(private http: HttpClient) { }
   
    
  public getAll(
    page?: number, itemsPerPage?: number, 
    descricao?: string, concessaoId?: number | null, tipoId?: number | null
    ): Observable<PaginatedResult<Documento[]>> {

      const paginatedResult: PaginatedResult<Documento[]> = new PaginatedResult<Documento[]>();

    let params = this.validarParams(page,itemsPerPage,descricao,concessaoId,tipoId);

    return this.http.get<any>(this.baseUrl, {observe: 'response', params})
    .pipe(take(1),
      map((response) => {
        paginatedResult.result = response.body;
          if(response.headers.has('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') || '{}');
          }
          return paginatedResult;
      }));
  }

  

  public getAllDoUsuario(
    page?: number, itemsPerPage?: number, 
    descricao?: string, concessaoId?: number | null, tipoId?: number | null
    ): Observable<PaginatedResult<Documento[]>>
     {
      const paginatedResult: PaginatedResult<Documento[]> = new PaginatedResult<Documento[]>();
      let params = this.validarParams(page,itemsPerPage,descricao,concessaoId,tipoId);


      return this.http.get<any>(`${this.baseUrl}/buscar/usuario` , {observe: 'response', params})
      .pipe(take(1),
      map((response) => {
        paginatedResult.result = response.body;
          if(response.headers.has('Pagination')) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') || '{}');
          }
          return paginatedResult;
      }));
  }

  public post(documento: Documento): Observable<any> {
    return this.http
      .post<any>(this.baseUrl, documento)
      .pipe(take(1));
  }
  public put(documento: Documento): Observable<any> {
    return this.http
      .put<any>(`${this.baseUrl}/${documento.id}`, documento)
      .pipe(take(1));
  }

  public postUpload(documentoId: number, file: File): Observable<any> {
    const fileToUpload = file as File;////
    const formData = new FormData();
    formData.append('file', fileToUpload);

    
    return this.http
      .post<any>(`${this.baseUrl}/upload-image/${documentoId}`, formData)
      .pipe(take(1));
  }

  public postFile(documentoId: number , fileToUpload: File ): Observable<any> {
    const file: FormData = new FormData();
    file.append('file', fileToUpload);

    return this.http.post(`${this.baseUrl}/file/${documentoId}`, fileToUpload).pipe(take(1));
  }

  public getFile(documentoId: number): any {
    return this.http.get<File>(`${this.baseUrl}/file/${documentoId}`).pipe(
      take(1),
      map((response) => {
        return response;
      })
    )
  }
  public getUrlFile(documentoId: number): any {
    return `${this.baseUrl}/file/${documentoId}`
  }

  public delete(id:number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${id}`).pipe(
      take(1),
      map((response) => {
        return response.result;
      })
    )
  }
  
  private validarParams(page?: number, itemsPerPage?: number, 
    descricao?: string, concessaoId?: number | null, tipoId?: number | null): HttpParams
    {
      let params = new HttpParams;

      if (page != null && itemsPerPage != null) {
        params = params.append('pageNumber', page.toString());
        params = params.append('pageSize', itemsPerPage.toString());
      }
  
      if (descricao != null && descricao != '')
        params = params.append('descricao', descricao)
  
      if (concessaoId != null && concessaoId != undefined && concessaoId != 0)
        params = params.append('concessaoId', concessaoId)
  
      if (tipoId != null && tipoId != undefined && tipoId != 0)
        params = params.append('tipoId', tipoId)

        return params;
  }
  
}

