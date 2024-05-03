import { postData, getData } from './api.js';

async function onLoadComments() {
    const comments = await getCommentsForTopic(1); // replace with your actual topic id
    const commentListElement = document.getElementById('comment-list');
    comments.forEach(comment => {
        const commentElement = document.createElement('div');
        commentElement.textContent = comment.text; // replace with your actual comment text property
        commentListElement.appendChild(commentElement);
    });
}


async function addComment(topicId, text) {
    const data = {
        topicId: topicId,
        text: text
    };
    const response = await postData('comments/add', data);
    if (response.status === 200) {
        console.log('Comment added successfully');
    } else {
        console.error('Error adding comment:', response);
    }
}

async function getCommentsForTopic(id) {
    const response = await getData(`comments/topic/${id}`);
    if (response.status === 200) {
        return response.json();
    } else {
        console.error('Error getting comments for topic:', response);
    }
}

export { addComment, getCommentsForTopic };
