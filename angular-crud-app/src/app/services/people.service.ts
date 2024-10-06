import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PeopleService {
  constructor(private _http: HttpClient) {}

  addPeople(data: any): Observable<any> {
    return this._http.post('https://localhost:7172/api/People', data);
  }

  updatePeople(id: number, data: any): Observable<any> {
    return this._http.put(`https://localhost:7172/api/People/${id}`, data);
  }

  getPeopleList(): Observable<any> {
    return this._http.get('https://localhost:7172/api/People');
  }

  deletePeople(id: number): Observable<any> {
    return this._http.delete(`https://localhost:7172/api/People/${id}`);
  }
}
