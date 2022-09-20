import React, {useEffect, useState}  from 'react';
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';
import GeneralAxiosService from '../../services/GeneralAxiosService';

const Matches = () => {
    const [pageApi, setPageApi] = useState(1);
    const [matches, setMatches] = useState({});
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Fixtures/All/" + pageApi)
        .then(res => setMatches(res.data))
        .then(() => setLoading(false));
    },[pageApi]);

    const matchesDisplay = () => {
        if(loading === false){
            return <tbody>
                {matches.pageResults.map((x, i) => {
                    return <tr key={i}>
                        <td>{x.homeTeam.teamName} vs {x.awayTeam.teamName}</td>
                    </tr>
                })}
            </tbody>
        }

        return <></>
    }

    return <section className='matchesPage'> 
        <table>
            <thead>
                <tr>
                    <td>Fixtures</td>
                    <td>Gameweek {pageApi}</td>
                </tr>
            </thead>
            {matchesDisplay()}
        </table>
        <Stack spacing={2}>
            <Pagination count={matches.totalPages} shape="rounded" onChange={(e, value) => setPageApi(value)}/>
        </Stack>
    </section>
}

export default Matches;