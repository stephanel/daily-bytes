package utils

import (
	"net/http"
	"text/template"

	"catdance/templates"
)

func CustomTemplateExecute(res http.ResponseWriter, req *http.Request, templateName string, data map[string]interface{}) {
	// Append common templates and data structs and execute template
	t, _ := template.New("").ParseFiles(templates.BASE, templateName)
	t.ExecuteTemplate(res, "base.html", data)
}
