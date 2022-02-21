import { Component, OnInit } from '@angular/core';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {
  private readonly PAGE_SIZE = 3;

  queryResult: any = {};
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'BookId' },
    { title: 'Title', key: 'title', isSortable: true },
    { title: 'Genre', key: 'author', isSortable: true },
    { title: 'Author', key: 'genre', isSortable: true },
    { title: 'Stock', key: 'stock', isSortable: false },
    { title: 'PublishDate', key: 'publishDate', isSortable: false },
  ];
  books: any;

  constructor(private bookService: BookService) { }

  ngOnInit() {
    this.populateBooks();
  }

  private populateBooks() {
    this.bookService.getBooks(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onFilterChange() {
    this.query.page = 1;
    this.populateBooks();
  }

  resetFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.populateBooks();
  }

  sortBy(columnName) {
    if (this.query.sortBy === columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateBooks();
  }

  onPageChange(page) {
    this.query.page = page;
    this.populateBooks();
  }

}
