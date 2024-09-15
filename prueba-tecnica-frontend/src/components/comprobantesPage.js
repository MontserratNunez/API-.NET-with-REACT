import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

const ComprobantesPage = () => {
  const { rncCedula } = useParams();
  const [contribuyente, setContribuyente] = useState(null);
  const [comprobantes, setComprobantes] = useState([]);
  const [totalITBIS, setTotalITBIS] = useState(0);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  const fetchContribuyente = async () => {
    try {
      const response = await axios.get(`http://localhost:5298/api/contribuyente/${rncCedula}`);
      setContribuyente(response.data.data.contribuyente);
    } catch (err) {
      setError('Error al cargar la información del contribuyente');
    }
  };

  const fetchComprobantes = async () => {
    try {
      const response = await axios.get(`http://localhost:5298/api/contribuyente/${rncCedula}/comprobantes`);
      const { data } = response.data;
      setComprobantes(data.comprobantes);
      setTotalITBIS(data.totalITBIS);
    } catch (err) {
      setError('Error al cargar los comprobantes');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchContribuyente();
    fetchComprobantes();
  }, [rncCedula]);

  if (loading) {
    return <div>Cargando...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <div>
      <h2>Comprobantes del Contribuyente</h2>
      <div className='block'>
        <div>
            <h3 className='emphasis'>Información del Contribuyente</h3>
            <div className='card'>
                <p className='litle'><strong>Nombre:</strong></p>
                <p>{contribuyente.name}</p>
            </div>
            <div className='card'>
                <p className='litle'><strong>Tipo:</strong></p>
                <p>{contribuyente.type == "PERSONA FISICA" ? "PERSONA FÍSICA" : "PERSONA JURÍDICA"}</p>
            </div>
            <div className='card'>
                <p className='litle'><strong>Estatus:</strong></p>
                <p>{contribuyente.status}</p>
            </div>
            <div className='card'>
                <p className='litle'><strong>RNC/Cédula:</strong></p>
                <p>{contribuyente.rncCedula}</p>
            </div>
        </div>
      </div>
      <div className='block'>
        <h3 className='emphasis'>Comprobantes Fiscales</h3>
        {comprobantes.length > 0 ? (<div>
          <h4>Total ITBIS: {totalITBIS}</h4>
            <table className='med'>
                <tbody>
                <tr>
                    <th><strong>RNC/Cédula:</strong></th>
                    <th><strong>NCF:</strong></th>
                    <th><strong>Monto:</strong></th>
                    <th><strong>ITBIS18:</strong></th>
                </tr>
                {comprobantes.map(comprobante => (
                <tr className='table'>
                    <td>{comprobante.rncCedula}</td>
                    <td>{comprobante.ncf}</td>
                    <td>{comprobante.amount}</td>
                    <td>{comprobante.itbis}</td>
                </tr>))}
                </tbody>
            </table>
          </div>
        ) : <em>No se encontraron comprobantes</em>}
        </div>
    </div>
  );
};

export default ComprobantesPage;
