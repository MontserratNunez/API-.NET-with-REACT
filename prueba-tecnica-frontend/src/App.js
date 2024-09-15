import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import ContribuyentesLista from './components/contribuyentesLista';
import ComprobantesPage from './components/comprobantesPage';
import AddContribuyente from './components/addContribuyente';
import AddComprobante from './components/addComprobante';
import PaginaInicio from './components/inicio';

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<PaginaInicio />}/>
        <Route path="/lista-contribuyentes" element={<ContribuyentesLista />} />
        <Route path="/:rncCedula/comprobantes" element={<ComprobantesPage />} />
        <Route path="/add-contribuyente" element={<AddContribuyente/>} />
        <Route path="/add-comprobante" element={<AddComprobante/>} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
