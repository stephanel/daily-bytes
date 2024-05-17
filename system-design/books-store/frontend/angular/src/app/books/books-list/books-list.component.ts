import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Author, Book } from 'src/app/books/book.model';
import { BookService } from 'src/app/books/book.service';
import { AuthorConcatenationPipe } from 'src/app/books/authors-concatenation.pipe';

@Component({
  selector: 'app-books-list',
  standalone: true,
  imports: [CommonModule, RouterModule, AuthorConcatenationPipe],
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
