import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/local";
import { Book } from "src/app/books/book.model";

@Injectable({
    providedIn: 'root'
})
export class BookService {

    baseUrl = `${environment.baseUrl}/books`

    constructor(private httpClient: HttpClient) 
    { }

    get(id: number): Observable<Book> {
        // return of<Book>(
        // {
        //     id: 12,
        //     title: 'Design Patterns',
        //     isbn: '978-0201633610',
        //     authors: [
        //         { firstName: 'Erich', lastName: 'Gamma', knownFor: 'Gang of Four' },
        //         { firstName: 'Richard', lastName: 'Helm', knownFor: 'Gang of Four' },
        //         { firstName: 'Ralph', lastName: 'Johnson', knownFor: 'Gang of Four' },
        //         { firstName: 'John', lastName: 'Vlissides', knownFor: 'Gang of Four' }
        //     ],
        //     language: 'English',
        //     thumbnailUrl: 'https://images-na.ssl-images-amazon.com/images/I/41tWJh6D6YL._SX258_BO1,204,203,200_.jpg'
        // });

        return this.httpClient.get<Book>(`${this.baseUrl}/${id}`);
    }

    getAll(): Observable<Book[]> {
        return this.httpClient.get<Book[]>(this.baseUrl);
    }
}