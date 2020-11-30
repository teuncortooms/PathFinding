export function dijkstra(grid, startNode, finishNode) {
    if (!startNode || !finishNode || startNode === finishNode) return false; // TODO: throw exc in js?

    const visitedNodesInOrder = [];
    startNode.distance = 0;
    const unvisitedNodes = getAllNodes(grid);
    while (unvisitedNodes.length !== 0) {
        sortNodesByDistance(unvisitedNodes);
        const closestNode = unvisitedNodes.shift();
        closestNode.isVisited = true;
        visitedNodesInOrder.push(closestNode);
        if (closestNode === finishNode) return visitedNodesInOrder;
        updateUnvisitedNeighbours(closestNode, grid);
    }
}

function getAllNodes(grid) {
    const nodes = [];
    for (const row of grid) {
        for (const node of row) {
            nodes.push(node);
        }
    }
    return nodes;
}

function sortNodesByDistance(unvisitedNodes) {
    unvisitedNodes.sort((nodeA, nodeB) => nodeA.distance - nodeB.distance);
}

function updateUnvisitedNeighbours(node, grid) {
    const unvisitedNeighbours = getUnvisitedNeighbours(node, grid);
    for (const neighbour of unvisitedNeighbours) {
        neighbour.distance = node.distance + 1;
        neighbour.previousNode = node;
    }
}

function getUnvisitedNeighbours(node, grid) {
    const neighbours = [];
    const { col, row } = node;
    if (row > 0) neighbours.push(grid[row - 1][col]); // add above
    if (row < grid.length - 1) neighbours.push(grid[row + 1][col]); // add below
    if (col > 0) neighbours.push(grid[row][col - 1]); // add left
    if (col < grid[0].length - 1) neighbours.push(grid[row][col + 1]); // add right
    return neighbours.filter(neighbour => !neighbour.isVisited);
}