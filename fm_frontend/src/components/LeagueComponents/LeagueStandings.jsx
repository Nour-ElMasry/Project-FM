import { useEffect, useState } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import LeagueTableTeam from '../LeagueComponents/LeagueTableTeam';
import Loading from '../Loading';

const LeaugeStandings = (props) => {
    const [teams, setTeams] = useState({});
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Leagues/" + props.league.id + "/Teams")
        .then((res) => setTeams(res.data))
        .then(() => setLoading(false))
    }, [])

    return <>
        {loading && <Loading />}
        {!loading && <Table className='leagueTable tablesContainer container--styled'>
            <TableHead>
                <TableRow>
                    <TableCell>
                        <h4 className="tableHeaderTxt">TEAMS</h4>
                    </TableCell>
                    <TableCell align="center">
                        <h4 className="tableHeaderTxt">PL</h4>
                    </TableCell>
                    <TableCell className='tableColumnHide' align="center">
                        <h4 className="tableHeaderTxt">W</h4>
                    </TableCell>
                    <TableCell className='tableColumnHide' align="center">
                        <h4 className="tableHeaderTxt">D</h4>
                    </TableCell>
                    <TableCell className='tableColumnHide' align="center">
                        <h4 className="tableHeaderTxt">L</h4>
                    </TableCell>
                    <TableCell align="center">
                        <h4 className="tableHeaderTxt">GD</h4>
                    </TableCell>
                    <TableCell align="center">
                        <h4 className="tableHeaderTxt">PTS</h4>
                    </TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {teams.map((t, i) =>
                    <LeagueTableTeam key={i} 
                        team={t}
                        position={i+1}
                    />
                )}
            </TableBody>
            </Table>}
    </>
}

export default LeaugeStandings;