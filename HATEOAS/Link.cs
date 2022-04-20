namespace mniaAPI.HATEOAS
{
    //realiza o link das ações 
    public class Link
    {
        //o link da ação
        public string href { get; set; }
        //descreve o método
        public string rel { get; set; }
        //tipo de método
        public string method { get; set; }
        public Link(string href, string rel, string method)
        {
            this.href = href;
            this.rel = rel;
            this.method = method;
        }
    }

}