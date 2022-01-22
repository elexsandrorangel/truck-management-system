import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import { Truck } from '../models/truck.model';
import {Observable} from "rxjs";

const baseURL = `${environment.apiUrl}trucks`;
@Injectable({
  providedIn: 'root'
})
export class TruckService {

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<Truck[]> {
    return this.httpClient.get<Truck[]>(baseURL);
  }

  getById(id: string): Observable<any>{
    return this.httpClient.get(`${baseURL}/${id}`);
  }

  create(data: any): Observable<any> {
    return this.httpClient.post(baseURL, data);
  }

  update(id: string, data: any): Observable<any> {
    return this.httpClient.put(`${baseURL}/${id}`, data);
  }

  delete(id: string): Observable<any> {
    return this.httpClient.delete(`${baseURL}/${id}`);
  }
}
