import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BooksListComponent } from './books-list/books-list.component';

@Component({
  standalone: true,
  imports: [BooksListComponent, RouterModule],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Book Store';
}
