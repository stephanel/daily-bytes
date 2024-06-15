<?php
class View {

    private $template_content;

    private function __construct($template_path) {
        $this->template_content = file_get_contents($template_path);
    }

    function apply($flag, $value): View {
        $this->template_content = str_replace('{'.$flag.'}', $value, $this->template_content);
        return $this;
    }

    function echo(): void {
        echo $this->template_content;
    }

    static function create($template_path): View {
        return new View($template_path);
    }
}

?>