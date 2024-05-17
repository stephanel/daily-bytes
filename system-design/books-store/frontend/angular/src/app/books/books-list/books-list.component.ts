import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Author, Book } from 'src/app/books/book.model';
import { BookService } from 'src/app/books/book.service';

@Component({
  selector: 'app-books-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './books-list.component.html',
  styleUrl: './books-list.component.css',
})
export class BooksListComponent implements OnInit {
  books?: Book[];

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.bookService.getAll().subscribe(books => {
      this.books = books;
    });
  }

  formatAuthor(author: Author): string
  {
      return `${author.firstName} ${author.lastName}`;
  
  }

}
