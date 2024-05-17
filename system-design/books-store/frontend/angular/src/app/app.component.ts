import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BooksListComponent } from 'src/app/books/books-list/books-list.component';

@Component({
  standalone: true,
  imports: [BooksListComponent, RouterModule],
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Best Book';
}
