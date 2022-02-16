import { Component, OnInit } from '@angular/core';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {

  books: any;

  constructor(private bookService: BookService) { }

  ngOnInit() {
    this.bookService.getBooks().subscribe(
      (response) => {
        this.books = response;
      },
      (error) => {
        console.log(error);
      });
  }
}

interface Book {
  bookId: number;
  title: string;
  genre: string;
  author: string;
  stock: number;
  publishDate: string;
}

