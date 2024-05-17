import { Pipe, PipeTransform } from "@angular/core";
import { Author } from "./book.model";

@Pipe({
    name: 'authorsConcatenation',
    standalone: true
})
export class AuthorConcatenationPipe implements PipeTransform {

    transform(authors: Author[] | undefined): string | null {
        return authors == null ? null : authors.map(author => this.formatAuthor(author)).join(', ');
    }

    private formatAuthor(author: Author): string {
        return `${author.firstName} ${author.lastName}`;
    }
}