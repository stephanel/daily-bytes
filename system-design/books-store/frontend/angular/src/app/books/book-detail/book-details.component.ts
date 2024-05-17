import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Book } from 'src/app/books/book.model';
import { BookService } from 'src/app/books/book.service';

@Component({
  selector: 'app-book-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './book-details.component.html',
  styleUrl: './book-details.component.css',
})
export class BookDetailsComponent implements OnInit {
  bookDetails?: Book;

  constructor(
    private route: ActivatedRoute, 
    private bookService: BookService) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      let bookId = Number(params.get('id'));
      this.bookService.get(bookId).subscribe(book => this.bookDetails = book);
    });
  }

}
