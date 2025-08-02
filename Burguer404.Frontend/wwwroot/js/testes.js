import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    vus: 10000,
    duration: '60s',
};

export default function () {
    http.get('http://localhost:30081/api/ProdutoHandler/listar');
    sleep(1); 
}