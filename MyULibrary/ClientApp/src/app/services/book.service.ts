import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getBooks() {
    return this.http.get(this.baseUrl + 'api/Book/GetBooks');
  }
  
  getBook(id) {
    return this.http.get(this.baseUrl + 'api/Book/GetBook/' + id);
  }

  createBook(book) {
    return this.http.post(this.baseUrl + 'api/Book/Create', book);
  }

}