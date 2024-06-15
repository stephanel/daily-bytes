package controllers

import (
	"math/rand"
	"net/http"
	"time"

	"catdance/stores"
	"catdance/templates"
	"catdance/utils"
)

func HomeController(res http.ResponseWriter, req *http.Request) {
	images := stores.GetImages()

	data := make(map[string]interface{})

	s := rand.NewSource(time.Now().Unix())
	r := rand.New(s) // initialize local pseudorandom generator
	data["url"] = images[r.Intn(len(images))]
	controllerTemplate := templates.HOME
	if req.Method == "GET" {
		utils.CustomTemplateExecute(res, req, controllerTemplate, data)
	}
}
