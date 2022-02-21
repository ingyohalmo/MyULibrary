import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../models/book';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private readonly booksEndpoint = 'api/Book/';

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getBooks(filter) {
    return this.http.get(this.baseUrl + this.booksEndpoint + 'GetBooks' + '?' + this.toQueryString(filter));
  }

  getBook(id) {
    return this.http.get(this.baseUrl + this.booksEndpoint + 'GetBook/' + id);
  }

  createBook(book) {
    return this.http.post(this.baseUrl + this.booksEndpoint + 'Create', book);
  }

  updateBook(book: Book) {
    return this.http.put(this.baseUrl + this.booksEndpoint + 'Update/' + book.bookId, book);
  }

  deleteBook(bookId: number) {
    return this.http.delete(this.baseUrl + this.booksEndpoint + 'Delete/' + bookId);
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }
}