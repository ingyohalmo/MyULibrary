import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Book } from '../../models/book';
import { BookService } from '../../services/book.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.scss']
})
export class BookFormComponent implements OnInit {

  book: Book = {
    bookId: 0,
    title: "",
    genre: "",
    author: "",
    stock: 0,
    publishDate: new Date(Date.now()).toISOString()
  };

  constructor(
    private bookService: BookService,
    private notificationService: NotificationService,
    private route: ActivatedRoute,
    private router: Router) {

    this.route.params.subscribe(params => {
      this.book.bookId = +params['id'] || 0;
    });

  }

  ngOnInit() {
    if (this.book.bookId) {
      this.bookService.getBook(this.book.bookId).subscribe(
        x => {
          this.setBook(x);
        },
        error => {
          if (error.status == 404)
            this.router.navigate(['/list/books']);
        });
    }
  }

  formatDate(date) {
    var d = new Date(date),
      month = '' + (d.getMonth() + 1),
      day = '' + d.getDate(),
      year = d.getFullYear();

    if (month.length < 2)
      month = '0' + month;
    if (day.length < 2)
      day = '0' + day;

    return [year, month, day].join('-');
  }

  private setBook(book) {
    this.book.bookId = book.bookId;
    this.book.title = book.title;
    this.book.genre = book.genre;
    this.book.author = book.author;
    this.book.stock = book.stock;
    this.book.publishDate = book.publishDate;
  }

  submit() {
    this.book.publishDate = this.formatDate(this.book.publishDate);
    
    var result$ = (this.book.bookId) ? this.bookService.updateBook(this.book) : this.bookService.createBook(this.book);
    
    result$.subscribe(book => {
      this.notificationService.showSuccess("Book saved successfully", "Success");
      this.router.navigate(['/books/', this.book.bookId]);
    },
      error => {
        this.notificationService.showError(error.error, "Error");
      });
  }

  delete() {
    if (confirm("Are you sure you want to delete this book?")) {
      this.bookService.deleteBook(this.book.bookId).subscribe(
        x => {
          this.notificationService.showSuccess("Book deleted successfully", "Success");
          this.router.navigate(['/list/books']);
        });
    }
  }

}
