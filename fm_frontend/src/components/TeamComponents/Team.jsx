import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';

const Team = (props) => {
    const params = useParams();
    const [team, setTeam] = useState({});
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Teams/" + params.id)
        .then((response) => setTeam(response.data))
        .then(() => setLoading(false))
    }, [params])

    return <section className="teamSection container container--pa">
        <h1 className='title'>Team Details</h1>

        {loading && <Box sx={{textAlign: "center", marginTop: "10rem"}}>
                <CircularProgress style={ {width: "3rem", height: "3rem"}} />
            </Box>}

        {!loading && console.log(team)}
           
    </section>
}

export default Team;