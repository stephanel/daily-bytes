import { Route } from '@angular/router';
import { BooksListComponent } from './books/books-list/books-list.component';
import { BookDetailsComponent } from './books/book-detail/book-details.component';
import { UserAccountComponent } from './users/user-account/user-account.component';
import { UserOrdersHistoryComponent } from './user-orders-history/user-orders-history/user-orders-history.component';

export const appRoutes: Route[] = [
    { path: '', component: BooksListComponent },
    { path: 'book/:id', component: BookDetailsComponent },
    { path: 'user/:id', component: UserAccountComponent },
    { path: 'user/:id/order-history', component: UserOrdersHistoryComponent },
];