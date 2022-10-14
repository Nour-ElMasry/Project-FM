import { useEffect, useState } from 'react';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import LeagueTableTeam from '../LeagueComponents/LeagueTableTeam';
import Loading from '../Loading';
import { Link } from 'react-router-dom';

const MatchTeamsLeagueTable = (props) => {
    const [leagueTeams, setLeagueTeams] = useState({})
    const [loading, setLoading] = useState(true)

    const league = props.league
    const homeTeam = props.homeTeam
    const awayTeam = props.awayTeam

    useEffect(() => {
        GeneralAxiosService.getMethod('https://localhost:7067/api/v1/Leagues/'+league.leagueId+'/Teams')
        .then((response) => 
        setLeagueTeams(response.data))
        .then(() => setLoading(false))
    },[league])

    return <div>
        {loading && <Loading />}
        {!loading && <Table className='leagueTable tablesContainer container--styled'>
                <TableHead>
                    <TableRow>
                        <TableCell colSpan={4}>
                            <Link style={{color: "black", textDecoration:"underline", fontSize: "0.9rem"}} to={"/leagues/"+league.leagueId}>{league.leagueName} standings</Link>
                        </TableCell>
                        <TableCell align="center">
                            <h5>PL</h5>
                        </TableCell>
                        <TableCell className='tableColumnHide' align="center">
                            <h5>W</h5>
                        </TableCell>
                        <TableCell className='tableColumnHide' align="center">
                            <h5>D</h5>
                        </TableCell>
                        <TableCell className='tableColumnHide' align="center">
                            <h5>L</h5>
                        </TableCell>
                        <TableCell align="center">
                            <h5>GD</h5>
                        </TableCell>
                        <TableCell align="center">
                            <h5>PTS</h5>
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    <LeagueTableTeam 
                        team={leagueTeams[Math.min(
                            leagueTeams.findIndex(t => 
                                t.id === homeTeam.teamId &&
                                t.name === homeTeam.teamName &&
                                t.logo === homeTeam.teamLogo)
                            ,
                            leagueTeams.findIndex(t => 
                                t.id === awayTeam.teamId &&
                                t.name === awayTeam.teamName &&
                                t.logo === awayTeam.teamLogo)
                        )]} 
                        position={1 + Math.min(
                            leagueTeams.findIndex(t => 
                                t.id === homeTeam.teamId &&
                                t.name === homeTeam.teamName &&
                                t.logo === homeTeam.teamLogo)
                            ,
                            leagueTeams.findIndex(t => 
                                t.id === awayTeam.teamId &&
                                t.name === awayTeam.teamName &&
                                t.logo === awayTeam.teamLogo)
                        )}
                     />
                     <TableRow style={{cursor: 'unset'}}>
                        <TableCell style={{textAlign: 'center', padding: "0", paddingBottom: ".5rem"}} colSpan={10}>
                            <p>...</p>
                        </TableCell>
                     </TableRow>
                    <LeagueTableTeam 
                        team={leagueTeams[Math.max(
                            leagueTeams.findIndex(t => 
                                t.id === homeTeam.teamId &&
                                t.name === homeTeam.teamName &&
                                t.logo === homeTeam.teamLogo)
                            ,
                            leagueTeams.findIndex(t => 
                                t.id === awayTeam.teamId &&
                                t.name === awayTeam.teamName &&
                                t.logo === awayTeam.teamLogo)
                        )]} 
                        position={1 + Math.max(
                            leagueTeams.findIndex(t => 
                                t.id === homeTeam.teamId &&
                                t.name === homeTeam.teamName &&
                                t.logo === homeTeam.teamLogo)
                            ,
                            leagueTeams.findIndex(t => 
                                t.id === awayTeam.teamId &&
                                t.name === awayTeam.teamName &&
                                t.logo === awayTeam.teamLogo)
                        )}
                     />
                </TableBody>
            </Table>}
    </div>
}

export default MatchTeamsLeagueTable;
