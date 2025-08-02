import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    vus: 20,
    duration: '60s',
};

export default function () {
    http.get('http://localhost:5000/api/ClienteHandler/autenticar/cliente');
    sleep(1); 
}