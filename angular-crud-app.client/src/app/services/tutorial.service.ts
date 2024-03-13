import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tutorial } from '../models/tutorial.model';

// Create base url string
const baseUrl = 'https://localhost:7020';

@Injectable({
  providedIn: 'root'
})
export class TutorialService {

  constructor(private http: HttpClient) { }

  // get request
  getAll(): Observable<Tutorial[]> {
    return this.http.get<Tutorial[]>(baseUrl + '/api/tutorials');
  }

  // get request with id
  get(id: any): Observable<Tutorial> {
    return this.http.get<Tutorial>(baseUrl + '/api/tutorials/' + id);
  }

  // post request
  create(data: any): Observable<any> {
    return this.http.post(baseUrl + '/api/tutorials/', data);
  }

  // update request
  update(id: any, data: any): Observable<any> {
    return this.http.put(baseUrl + '/api/tutorials/' + id, data);
  }

  // delete request by id
  delete(id: any): Observable<any> {
    return this.http.delete(baseUrl + '/api/tutorials/' + id);
  }

  // delete request
  deleteAll(): Observable<any> {
    return this.http.delete(baseUrl + '/api/tutorials');
  }

  // get request with title
  findByTitle(title: any): Observable<Tutorial[]> {
    return this.http.get<Tutorial[]>(baseUrl + '/api/tutorials/' + title);
  }
}