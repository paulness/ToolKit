// AlgorithmTestingGroundC++.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

struct Node
{
	int key;
	Node* left, *right;
};


int findTreeHeight(Node* node)
{
	if (node == nullptr)
		return 0;

	int leftPathHeight = findTreeHeight(node->left);
	int rightPathHeight = findTreeHeight(node->right);
	return leftPathHeight > rightPathHeight ? leftPathHeight + 1 : rightPathHeight + 1;
}


int main()
{
	Node* n1 = new Node();
	n1->key = 1;

	Node* n2 = new Node();
	n2->key = 2;

	Node* n3 = new Node();
	(*n3).key = 3;

	Node* n4 = new Node();
	(*n4).key = 4;

	Node* n5 = new Node();
	(*n5).key = 4;

	Node* n6 = new Node();
	(*n6).key = 4;

	n1->left = n2;
	n2->right = n3;
	n3->left = n4;
	n4->left = n5;
	n5->left = n6;

	printf("" + findTreeHeight(n1));

    return 0;
}

