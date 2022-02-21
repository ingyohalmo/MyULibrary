import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from '../../services/book.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.scss']
})
export class BookFormComponent implements OnInit {

  book: any = {
    bookId: 0,
    title: "",
    genre: "",
    author: "",
    stock: 0,
    publishDate: Date.now()
  };

  constructor(
    private bookService: BookService,
    private notificatioService: NotificationService,
    private route: ActivatedRoute,
    private router: Router) {

    route.params.subscribe(params => {
      this.book.bookId = +params['id'];
    });

  }

  ngOnInit() {
    this.bookService.getBook(this.book.bookId).subscribe(x => this.book = x);
  }

  submit() {
    this.bookService.createBook(this.book)
      .subscribe(x => this.notificatioService.showSuccess("Book created successfully", "Success"));
  }

}
