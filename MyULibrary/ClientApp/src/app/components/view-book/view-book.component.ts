import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.component.html',
  styleUrls: ['./view-book.component.scss']
})
export class ViewBookComponent implements OnInit {

  book: any = {
    bookId: 0,
    title: "",
    genre: "",
    author: "",
    stock: 0,
    publishDate: Date.now().toString()
  };

  bookId: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bookService: BookService) {

    route.params.subscribe(p => {
      this.bookId = +p['id'];
      if (isNaN(this.bookId) || this.bookId <= 0) {
        router.navigate(['/books']);
        return;
      }
    });

  }

  ngOnInit() {
    this.bookService.getBook(this.bookId)
      .subscribe(
        b => this.book = b,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/books']);
            return;
          }
        });
  }

}
