<?php

include('stores.php');
include('view.class.php');

$i = rand(0, sizeof($images)-1);
$url = $images[$i];

View::create('index.view')
    ->apply('url', $url)
    ->echo();

?>